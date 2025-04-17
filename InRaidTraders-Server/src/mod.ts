/* eslint-disable @typescript-eslint/naming-convention */
import path from "path";
import fs from "fs";

import type { DependencyContainer } from "tsyringe";
import type { IPostDBLoadMod } from "@spt/models/external/IPostDBLoadMod";
import { IDatabaseTables } from "@spt/models/spt/server/IDatabaseTables";
import { FileSystemSync } from "@spt/utils/FileSystemSync";
import { ILogger } from "@spt/models/spt/utils/ILogger";
import { IPreSptLoadMod } from "@spt/models/external/IPreSptLoadMod";
import { DatabaseServer } from "@spt/servers/DatabaseServer";

class InRaidTraders implements IPreSptLoadMod, IPostDBLoadMod
{    
    public RootPath: string = path.join(path.dirname(__filename), "..", "..");
    public ModPath: string = path.join(this.RootPath, "InRaidTraders-Server");
    public DataPath: string = path.join(this.ModPath, "data");
    public LocaleRootPath: string = path.join(this.DataPath, "Locales");
    public database: IDatabaseTables;
    public vfs: FileSystemSync;
    public logger: ILogger;

    public preSptLoad(container: DependencyContainer): void
    {
        this.logger = container.resolve<ILogger>("WinstonLogger");
        this.vfs = container.resolve<FileSystemSync>("FileSystemSync");
    }
    public postDBLoad(container: DependencyContainer): void 
    {
        this.database = container.resolve<DatabaseServer>("DatabaseServer").getTables();
        this.logger.info(this.LocaleRootPath);
        this.importAllLocaleData();
        
    }
        /**
     * Loads and parses a config file from disk
     * @param fileName File name inside of config folder to load
     */
        public loadJsonFile<T>(filePath: string, readAsText = false): T
        {
            const file = path.join(filePath);
            const string = this.vfs.read(file);
    
            return readAsText 
                ? string as T
                : JSON.parse(string) as T;
        }
    private importAllLocaleData(): void
    {
        const localesPath = this.LocaleRootPath;
        const subDirs = fs.readdirSync(localesPath);

        for (const lang of subDirs)
        {
            const langDir = path.join(localesPath, lang);
            const localeFiles = fs.readdirSync(langDir);


            let entries = 0;

            for (const file of localeFiles)
            {
                const localeData = this.loadJsonFile<Record<string, string>>(path.join(langDir, file));
      
                entries += this.importLocaleData(lang, localeData);
            }

            if (entries === 0) continue;
        }

        this.importMissingLocalesAsEnglish();
    }

    private importLocaleData(lang: string, localeData: Record<string, string>): number
    {
        const globals = this.database.locales.global;

        for (const entry in localeData)
        {
            globals[lang][entry] = localeData[entry];
        }

        return Object.keys(localeData).length;
    }

    private importMissingLocalesAsEnglish(): void
    {
        const globals = this.database.locales.global;

        let count = 0;

        for (const entry in globals.en)
        {
            for (const lang in globals)
            {
                if (globals[lang][entry] === undefined)
                {
                    globals[lang][entry] = globals.en[entry];
                    count++;
                }
            }  
        }
    }
}

module.exports = { mod: new InRaidTraders() }