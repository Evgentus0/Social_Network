import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {map, tap} from 'rxjs/operators';
import { IUserResponse, UserSearch } from '../models/search-user.model';
import { environment } from 'src/environments/environment';


@Injectable()
export class UserSerchService {

  constructor(private http: HttpClient) {}

  search(filter: {name: string} = {name: ''}, page = 1): Observable<IUserResponse> {
    return this.http.get<IUserResponse>(environment.searchUser)
    .pipe(
      tap((response: IUserResponse) => {
        response.results = response.results
          .map(user => new UserSearch(
            user.id, 
            user.name
            ))
          // Not filtering in the server since in-memory-web-api has somewhat restricted api
          .filter(user => user.name.includes(filter.name))

        return response;
      })
      );
  }
}
