export class ProfileModel {
    name!: string;
    email!: string;
    userName!: string;
    oldPassword: string = '';
    password: string = '';
    confirmPassword: string = '';
} 