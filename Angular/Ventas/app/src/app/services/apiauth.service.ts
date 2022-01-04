import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Response } from '../models/response';
import { Usuario } from '../models/usuario';
import { map } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
  };

@Injectable({ providedIn: 'root' })
export class ApiauthService {
    url: string = 'https://localhost:44325/api/user/login';
    private usuarioSubject: BehaviorSubject<Usuario>;
    public get usuarioData(): Usuario{
        return this.usuarioSubject.value;
    }

    constructor(private http: HttpClient) { 
        //localStorage.getItem() puede devolver una cadena o null. JSON.parse() requiere una cadena
        //! para que no tome null o undefined
        this.usuarioSubject = new BehaviorSubject<Usuario>(JSON.parse(localStorage.getItem('usuario')! ));
    }

    login(email: string, password: string): Observable<Response> {
        return this.http.post<Response>(this.url, { email, password }, httpOptions).pipe(
            map(response => {
                if (response.exito === 1) {
                    const usuario: Usuario = response.data;
                    localStorage.setItem('usuario', JSON.stringify(usuario));
                    this.usuarioSubject.next(usuario);
                }
                return response;
            })
        );
    }

    logout() {
        localStorage.removeItem('usuario');
        this.usuarioSubject.next({email: "", token: ""});// no permitió colocar null
    }

}