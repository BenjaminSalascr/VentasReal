import { Component } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { MatDialogRef } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Concepto } from "src/app/models/concepto";
import { Venta } from "src/app/models/venta";
import { ApiVentaService } from "src/app/services/api-venta.service";

@Component({
    templateUrl: "./dialogventa.component.html"
})
export class DialogVentaComponent {

    public venta: Venta;
    public _conceptos: Concepto[];

    public conceptoForm = this.formBuilder.group({
        Cantidad: [0, Validators.required],
        Importe: [0, Validators.required],
        IdProducto: [1, Validators.required]
    });

    constructor(public dialogRef: MatDialogRef<DialogVentaComponent>,
        public snackBar: MatSnackBar,
        private formBuilder: FormBuilder,
        public apiVenta: ApiVentaService) {
        this._conceptos = [];
        this.venta = { IdCliente: 1, Conceptos: [] };
    }

    close() {
        this.dialogRef.close();
    }

    addConcepto() {
        this._conceptos.push(this.conceptoForm.value);
    }

    addVenta() {
        this.venta.Conceptos = this._conceptos;
        this.apiVenta.add(this.venta).subscribe(response => {
            this.dialogRef.close();
            this.snackBar.open('venta exitosa', '', { duration: 2000 });
        });
    }

}