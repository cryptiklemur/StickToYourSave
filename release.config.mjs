import { readFileSync } from 'node:fs';

import { declaredVersions, releaseConfig } from '@rimworks/mod-ci';

/** @type {import('semantic-release').GlobalConfig} */
export default releaseConfig({
    solution: 'Source/StickToYourSave/StickToYourSave.csproj',
    versions: declaredVersions(readFileSync('loadFolders.xml', 'utf8')),
    branches: ['main'],
    mods: [
        {
            name: 'StickToYourSave',
            title: 'Stick To Your Save',
            workshopId: '3765032020',
        },
    ],
    assets: [{ path: './dist/StickToYourSave-*.zip', label: 'Stick To Your Save (drop into RimWorld/Mods)' }],
});
