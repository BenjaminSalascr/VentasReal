import { Component } from "@angular/core";
import { MatDialogRef } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Cliente } from "src/app/models/Cliente";
import { ApiClienteService } from "src/app/services/api-cliente.service";

@Component({   // decorador
    templateUrl: './dialogcliente.component.html',
    //styleUrls: ['./dialogcliente.component.css']
})

export class DialogClienteComponent {
    public nombre: string = '';

    constructor(
        public dialogRef: MatDialogRef<DialogClienteComponent>,
        public apiCliente: ApiClienteService,
        public snackBar: MatSnackBar
    ) { }

    close() {
        this.dialogRef.close();
    }

    addCliente() {
        const cliente: Cliente = { nombre: this.nombre };
        this.apiCliente.add(cliente).subscribe(response => {
            if (response.exito === 1) {
                this.dialogRef.close();
                this.snackBar.open('Cliente agregado', '', { duration: 2000 });
            }
        });
    }
}