export interface Create {
    firstName: string;
    lastName: string;
    email: string;
    mobile: string;
    address: string;
}

export interface CreateValidator extends AbstractValidator<Create> {

}

export interface IdOnly {
    id: number;
}