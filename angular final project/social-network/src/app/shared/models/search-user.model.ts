
export class UserSearch {
    constructor(public id: string, public name: string) {}
  }
  

export interface IUserResponse {
    total: number;
    results: UserSearch[];
}