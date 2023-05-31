export namespace CustomerModel {
    export interface Listing {
        id: number;
        fullName: string;
    }
    export interface DetailedView {
        firstName: string;
        lastName: string;
        email: string;
        mobile: string;
        address: string;
    }
}