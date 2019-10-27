import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Country } from '../Models/country.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { CityModel } from '../Models/city.model';
import { RelationshipModel } from '../Models/relationship.model';
import { MessageHeaderModel } from '../Models/messageHeader.model';
import { MessageHeaderType } from '../Models/messageHeaderType.model';

@Injectable()
export class AdditionalService{
    constructor(private http:HttpClient){}

    getCountries():Observable<Country[]>{
        let url=environment.additional+"/country";
        return this.http.get<Country[]>(url);
    }

    getCities():Observable<CityModel[]>{
        let url=environment.additional+"/city";
        return this.http.get<CityModel[]>(url);
    }

    getRelationship():Observable<RelationshipModel[]>{
        let url=environment.additional+"/relationship";
        return this.http.get<RelationshipModel[]>(url);
    }

    getMessageHeaderType():Observable<MessageHeaderType[]>{
        let url=environment.additional+"/messageheadertype";
        return this.http.get<MessageHeaderType[]>(url);
    }
}