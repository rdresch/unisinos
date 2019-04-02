import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/model/User';
import { AuthService } from 'src/app/services/authService';
import { Http } from '@angular/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public user = new User()

  constructor(private http : Http) { }

  ngOnInit() {
    this.cleanUser()
  }

  cleanUser(){
    this.user = new User()
  }

  login(){
    console.log(this.user)
    let Auth = new AuthService(this.http)
    Auth.login(this.user).then((response) => {
     
    })

  }

  defineName(text){
    this.user.name = ((<HTMLInputElement>text.target).value)
  }

  definePass(text){
    this.user.pass = ((<HTMLInputElement>text.target).value)
  }

}
