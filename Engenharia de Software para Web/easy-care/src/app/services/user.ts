import {User} from '../model/user'
import { Http, RequestOptions, Headers, Response } from '@angular/http'
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { Injectable } from '@angular/core';

@Injectable()
export class UserServices{
    constructor(private http: Http){

    }

    public getUser(){

    }

    public setUser(user: User): Observable<number>{

        let headers: Headers = new Headers()

        headers.append('Content-type', 'application/json',)

        return this.http.post("localhost:61141/usuarionovo", 
        user, 
        new RequestOptions({headers: headers})
        )
        .pipe(map((res: Response) => res.json().id))
    }


    public getAuthUser(user: User){
        
    }
}