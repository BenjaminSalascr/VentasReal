import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from '../models/response';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
  };

@Injectable({ providedIn: 'root' })
export class ApiauthService {
    url: string = 'https://localhost:44325/api/user/login';

    constructor(private http: HttpClient) { }

    login(email: string, password: string): Observable<Response> {
        return this.http.post<Response>(this.url, { email, password }, httpOptions);
    }


}