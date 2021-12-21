import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
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
        public snackBar: MatSnackBar,
        @Inject(MAT_DIALOG_DATA) public cliente: Cliente
    ) {
        if (this.cliente !== null) {
            this.nombre = cliente.nombre;
        }    
     }

    close() {
        this.dialogRef.close();
    }

    editCliente(){
        const cliente: Cliente = { nombre: this.nombre, id: this.cliente.id };
        this.apiCliente.edit(cliente).subscribe(response => {
            if (response.exito === 1) {
                this.dialogRef.close();
                this.snackBar.open('Cliente editado', '', { duration: 2000 });
            }
        });
    }

    addCliente() {
        const cliente: Cliente = { nombre: this.nombre, id: 0 };
        this.apiCliente.add(cliente).subscribe(response => {
            if (response.exito === 1) {
                this.dialogRef.close();
                this.snackBar.open('Cliente agregado', '', { duration: 2000 });
            }
        });
    }
}