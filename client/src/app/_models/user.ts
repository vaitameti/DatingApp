import { reduce } from "rxjs/operators";

export interface User{
    username: string;
    token: string;
    photoUrl: string;
}