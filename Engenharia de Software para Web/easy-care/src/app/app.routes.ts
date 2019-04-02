import {Routes} from '@angular/router'
import { LoginComponent } from './view/login/login.component';
import { NewComponent } from './view/new/new.component';
import { MainComponent } from './view/main/main.component';


export const ROUTES: Routes =
[
    {  path: 'login', component: LoginComponent},
    { path: 'new', component: NewComponent },
    { path: 'main', component: MainComponent },
    { path: '**', redirectTo: 'login'}
]