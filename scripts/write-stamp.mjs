#!/usr/bin/env node
import { writeStamp } from '@rimworks/mod-ci';

process.stdout.write(await writeStamp({ solution: 'StickToYourSave.slnx' }));
