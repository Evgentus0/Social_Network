import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MessageHeaderModel } from '../Models/messageHeader.model';
import { environment } from 'src/environments/environment';

@Injectable()
export class MessageHeaderService{

    constructor(private http: HttpClient){}

    getMessageHeaders():Observable<MessageHeaderModel[]>{
        return this.http.get<MessageHeaderModel[]>(environment.messageHeader);
    }

    createMessageHeader(headerModel:MessageHeaderModel, users:string[]){

        let usersId:string="[";
        users.forEach(element => {
            usersId+=`{"Id":"${element}"},`
        });
        usersId=usersId.slice(0, usersId.length-1);
        usersId+="]";
        const body=`{"Header":"${headerModel.header}",
        "Type":{"Id":${headerModel.typeId}},
        "Users":${usersId}}`;

        let header=new HttpHeaders({
            "Content-Type": "text/json",
        });
        let options={headers:header};
        return this.http.post(environment.messageHeader, body, options);
    }

    deleteMessageHeader(id:number){
        let url=environment.messageHeader+"/"+id;
        return this.http.delete(url);
    }
    deleteForOne(id:number){
        let url=environment.message+"/forone/"+id;
        return this.http.delete(url);
    }
}