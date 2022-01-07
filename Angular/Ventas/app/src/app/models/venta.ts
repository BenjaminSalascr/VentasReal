import { Concepto } from "./concepto";

export interface Venta {
    IdCliente: number;
    Conceptos: Concepto[];
}