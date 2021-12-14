import { Component, OnInit } from '@angular/core';
import { ApiClienteService } from '../services/api-cliente.service';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {

  constructor( private apiCliente: ApiClienteService) 
  { 
    apiCliente.getClientes().subscribe(response=>{
      console.log(response);
    });
  }

  ngOnInit(): void {
  }

}
