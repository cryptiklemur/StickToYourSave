/** @type {import('semantic-release').GlobalConfig} */
export default {
    branches: ['main'],
    plugins: [
        [
            '@semantic-release/commit-analyzer',
            {
                releaseRules: [
                    { type: 'refactor', release: 'patch' },
                    { type: 'style', release: 'patch' },
                    { type: 'ci', release: 'patch' },
                    { type: 'docs', release: 'patch' },
                ],
            },
        ],
        '@semantic-release/release-notes-generator',
        [
            '@semantic-release/exec',
            {
                prepareCmd:
                    'node scripts/write-stamp.mjs && dotnet build Source/StickToYourSave/StickToYourSave.csproj -c Release -p:Version=${nextRelease.version} && npx package-mod StickToYourSave ${nextRelease.version}',
            },
        ],
        [
            '@semantic-release/github',
            {
                assets: [
                    { path: './dist/StickToYourSave-*.zip', label: 'Stick To Your Save (drop into RimWorld/Mods)' },
                ],
            },
        ],
        [
            'semantic-release-steam',
            {
                appId: '294100',
                branchTargets: { main: 'stable' },
                mods: [
                    {
                        name: 'StickToYourSave',
                        path: '.',
                        previewfile: new URL('./About/Preview.png', import.meta.url).pathname,
                        workshopIds: { stable: '3765032020' },
                    },
                ],
            },
        ],
    ],
};
