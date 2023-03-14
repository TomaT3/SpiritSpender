
export interface INcCommand {
    command: string;
}

export class NcCommand implements INcCommand {

    command: string;
    
    constructor(ncCommandString: string) {
        this.command = ncCommandString;
    }
    
}