import { Product } from "./product";

export class Customer{
    id!: number;
    name!: string;
    email!: string;
    age!: string;
    city!: string;
    gender!: string;
    products!: Product[];
}
