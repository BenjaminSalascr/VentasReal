import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from '../models/response';

@Injectable({
  providedIn: 'root'
})
export class ApiClienteService {

  url : string = 'https://localhost:44325/api/cliente';

  constructor( private _http: HttpClient ) { }

  getClientes(): Observable<Response>
  {
    return this._http.get<Response>(this.url);
  }
}


