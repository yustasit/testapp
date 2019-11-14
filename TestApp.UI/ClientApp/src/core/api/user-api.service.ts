import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { BaseApiService } from './base-api.service';
import { UserModel } from 'core/models';
import { PaginationModel } from '../models/pagination.model';



@Injectable()
export class UserApiService extends BaseApiService {

    constructor(
        private _http: HttpClient) {
        super(_http);
    }

    public getUsers(sort?: string, pageIndex: number = 0, pageSize: number = 10): Observable<PaginationModel<UserModel>> {
        return this.get<PaginationModel<UserModel>>('api/user?sort=' + (sort ? sort : '') + '&itemsPerPage=' + pageSize + '&page=' + pageIndex);
    };

    public updateUser(user: UserModel): Observable<any> {
        return super.put<any>('api/user', user);
    }

    public addUser(user: UserModel): Observable<UserModel> {
        return super.post<UserModel>('api/user', user);
    }

    public deleteUser(userId: number): Observable<any> {
        return super.delete<any>('api/user?id=' + userId);
    }

}
