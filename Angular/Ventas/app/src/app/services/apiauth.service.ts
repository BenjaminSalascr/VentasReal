import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Response } from '../models/response';
import { Usuario } from '../models/usuario';
import { map } from 'rxjs/operators';
import { Login } from '../models/login';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
};

@Injectable({ providedIn: 'root' })
export class ApiauthService {
    url: string = 'https://localhost:44325/api/user/login';
    private usuarioSubject: BehaviorSubject<Usuario | null>;
    public usuario: Observable<Usuario | null>;

    public get usuarioData(): Usuario {
        return this.usuarioSubject.value!;
    }

    constructor(private http: HttpClient) {
        //localStorage.getItem() puede devolver una cadena o null. JSON.parse() requiere una cadena        
        this.usuarioSubject = new BehaviorSubject<Usuario | null>(JSON.parse(localStorage.getItem('usuario') || 'null'));
        this.usuario = this.usuarioSubject.asObservable();
    }

    login(login: Login): Observable<Response> {
        return this.http.post<Response>(this.url, login, httpOptions).pipe(
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
        this.usuarioSubject.next(null);// no permitió colocar null
    }

}