import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PublicationModel } from '../Models/publication.model';
import { environment } from 'src/environments/environment';

@Injectable()
export class PublicationService{
    constructor(private http:HttpClient){}
    
    getHomePublications(userId:string):Observable<PublicationModel[]>{
        let url=environment.publications+"/home/"+userId;
        return this.http.get<PublicationModel[]>(url);
    }

    getMainPublications():Observable<PublicationModel[]>{
        let url=environment.publications+"/main/"+sessionStorage.getItem("userName");
        return this.http.get<PublicationModel[]>(url);
    }

    createPublication(model:PublicationModel){
        const body={
            Header:model.header,
            Content:model.content,
        }
        let header=new HttpHeaders({
            "Content-Type": "text/json",
        });
        let options={headers:header};

        return this.http.post(environment.publications, body, options);
    }

    likePublication(id:number){
        const body={
            PublicationId:id,
        };
        let header=new HttpHeaders({
            "Content-Type": "text/json",
        });
        let options={headers:header};
        let url=environment.publications+"/like";
        return this.http.put(url, body, options);
    }

    deletePublication(id:number){
        let url=environment.publications+"/"+id;
        return this.http.delete(url);
    }

    editPublication(model:PublicationModel){
        const body={
            Header:model.header,
            Content:model.content,
            Id:model.id,
        }
        let header=new HttpHeaders({
            "Content-Type": "text/json",
        });
        let options={headers:header};

        return this.http.put(environment.publications, body, options);
    }
    
}