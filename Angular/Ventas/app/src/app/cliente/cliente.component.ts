import { Component, OnInit } from '@angular/core';
import { ApiClienteService } from '../services/api-cliente.service';
import { Response } from '../models/response';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {

  public lst: any[] = [];
  public columnas: string[] = ['id', 'nombre'];

  constructor(private apiCliente: ApiClienteService) { }

  ngOnInit(): void {
    this.getClientes();
  }

  getClientes() {
    this.apiCliente.getClientes().subscribe(response => {
      this.lst = response.data;
    });
  }

}
