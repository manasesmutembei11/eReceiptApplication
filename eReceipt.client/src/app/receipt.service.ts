import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class ReceiptService {
    private apiUrl = 'https://localhost:7194/api';

    constructor(private http: HttpClient) { }

    getInvoice(): Observable<string> {
        return this.http.get(`${this.apiUrl}/invoice-report`, { responseType: 'text' });
    }
    downloadInvoice(): Observable<Blob> {
        return this.http.get(`${this.apiUrl}/download`, { responseType: 'blob' });
    }

}