import { Injectable } from "@angular/core";
import { User } from '../Models/user.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, observable } from 'rxjs';
import { UserLoginModel } from '../Models/userLoginModel';
import { UserRegisterModel } from '../models/user-register.model';

@Injectable()
export class UserService{
    constructor(private http:HttpClient){}
    
    getUser():Observable<User>{
        return this.http.get<User>(environment.currentUserUrl);
    }

    getUserById(id:string):Observable<User>{
        let url=environment.currentUserUrl+"/"+id;
        return this.http.get<User>(url);
    }

    loginUser(login:UserLoginModel){
        const body=`grant_type=password&username=${login.email}&password=${login.password}`
        let headers = new HttpHeaders({
            "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8",
            "X-Requested-With": "XMLHttpRequest",
            "Accept": "application/json"
        });
        let options = { headers: headers };
        return this.http.post(environment.loginUser, body, options);
    }

    register(register:UserRegisterModel){
        const body={
            Name : register.name,
            Surname : register.surname,
            Gender : register.gender,
            DateOfBirth : register.dateBirth,
            PersonalInfo : register.personalInfo,
            RelationShipId : register.relationshipId,
            CountryId : register.countryId,
            CityId :  register.cityId,
            PictureProfilePath : "",
            Email : register.email,
            PhoneNumber : register.phoneNumber,
            Password : register.password,
            ConfirmPassword:register.confirmPassword,
        }
        let header=new HttpHeaders({
            "Content-Type": "text/json",
        });
        let url=environment.currentUserUrl+"/register";
        let options={headers:header};
        return this.http.post(url, body, options);
    }

    getFollowersById(id:string):Observable<User[]>{
        let url=environment.currentUserUrl+"/followers/"+id;
        return this.http.get<User[]>(url);
    }

    getFollowingById(id:string):Observable<User[]>{
        let url=environment.currentUserUrl+"/following/"+id;
        return this.http.get<User[]>(url);
    }

    subscribe(id:string){
        let url=environment.currentUserUrl+"/subscribe"
        const body=`"${id}"`;
        let header=new HttpHeaders({
            "Content-Type": "text/json",
        });
        let options={headers:header};
        return this.http.put(url, body, options);
    }

    unsubscribe(id:string){
        let url=environment.currentUserUrl+"/unsubscribe"
        const body=`"${id}"`;
        let header=new HttpHeaders({
            "Content-Type": "text/json",
        });
        let options={headers:header};
        return this.http.put(url, body, options);
    }

    getAll():Observable<User[]>{
        let url=environment.currentUserUrl+"/all";
        return this.http.get<User[]>(url);
    }

    getUsersByCity(city:string):Observable<User[]>{
        let url=environment.currentUserUrl+"/GetByCity/"+city;
        return this.http.get<User[]>(url);
    }

    delete(id:string){
        let url=environment.currentUserUrl+"/"+id;
        return this.http.delete(url);
    }
}