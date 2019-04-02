import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { User } from 'src/app/model/User';
import { UserServices } from 'src/app/services/user';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['../login/login.component.css']
})
export class NewComponent implements OnInit {
  user = new User()

  constructor(private http : Http) { 
    
  }
  ngOnInit() {
    this.cleanUser()
  }

  cleanUser(){
    this.user = new User()
  }

  defineName(text){
    this.user.name = ((<HTMLInputElement>text.target).value)
  }

  definePass(text){
    this.user.pass = ((<HTMLInputElement>text.target).value)
  }

  defineMail(text){
    this.user.mail = ((<HTMLInputElement>text.target).value)
  }

  finish(){
    let usrSv = new UserServices(this.http)
    usrSv.setUser(this.user).subscribe((id: number)=> {console.log(id)})
  }
}
