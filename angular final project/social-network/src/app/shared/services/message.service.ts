import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { MessageModel } from '../models/message.model';

@Injectable()
export class MessageService{
    constructor(private http:HttpClient){}

    getMessageByMessageHeaderId(id:number):Observable<MessageModel[]>{
        let url=environment.message+"/header/"+id;
        return this.http.get<MessageModel[]>(url);
    }

    sendMessage(message:MessageModel){
        const body={
            "Content":message.content,
            "MessageHeader":{Id:message.messageHeaderId},
            "Sender":{Id:message.senderId}
        }
        let headers=new HttpHeaders({
            "Content-Type": "text/json"
        });
        let options={headers:headers};
        
        return this.http.post(environment.message, body, options);
    }
}