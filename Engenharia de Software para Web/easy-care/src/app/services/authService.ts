import { Http, Headers } from '@angular/http';
import { User } from '../model/User';
import { Observable } from 'rxjs';


export class AuthService{
    constructor(private http: Http){

    }

    login(user: User){
        try{
     return this.http.post("localhost:61141/usuario", 
     JSON.stringify({username: user.name, pass: user.pass})).toPromise()
    }catch(e){
        alert('error')
    }
    }
}