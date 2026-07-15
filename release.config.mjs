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
                ],
            },
        ],
        '@semantic-release/release-notes-generator',
        [
            '@semantic-release/exec',
            {
                prepareCmd:
                    'dotnet build Source/StickToYourSave/StickToYourSave.csproj -c Release -p:Version=${nextRelease.version}',
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
                        workshopIds: { stable: '3765032020' },
                        previewfile: 'About/Preview.png',
                    },
                ],
            },
        ],
    ],
};
