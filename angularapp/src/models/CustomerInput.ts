export namespace CustomerInput {
    export interface Create {
        firstName: string;
        lastName: string;
        email: string;
        mobile: string;
        address: string;
    }
    export interface IdOnly {
        id: number;
    }
}