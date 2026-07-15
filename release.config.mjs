const workshopId = 'REPLACE_WITH_WORKSHOP_ID';

const plugins = [
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
];

if (workshopId !== 'REPLACE_WITH_WORKSHOP_ID') {
    plugins.push([
        'semantic-release-steam',
        {
            appId: '294100',
            branchTargets: { main: 'stable' },
            mods: [
                {
                    name: 'StickToYourSave',
                    path: '.',
                    workshopIds: { stable: workshopId },
                    previewfile: 'About/Preview.png',
                },
            ],
        },
    ]);
}

/** @type {import('semantic-release').GlobalConfig} */
export default {
    branches: ['main'],
    plugins,
};
