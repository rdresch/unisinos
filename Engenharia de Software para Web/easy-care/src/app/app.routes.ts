import {Routes} from '@angular/router'
import { LoginComponent } from './view/login/login.component';


export const ROUTES: Routes =
[
    {  path: 'login', component: LoginComponent},
    { path: '**', redirectTo: 'login'}
]