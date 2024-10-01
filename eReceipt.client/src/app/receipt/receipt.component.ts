import { Component, OnInit } from '@angular/core';
import { ReceiptService } from '../receipt.service';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-receipt',
  templateUrl: './receipt.component.html',
  styleUrl: './receipt.component.css'
})
export class ReceiptComponent implements OnInit {
  invoice: SafeHtml | any;
  constructor(private receiptService: ReceiptService,
    private sanitizer: DomSanitizer
  ) { }

  ngOnInit() {
    this.getInvoice();
  }

  getInvoice() {
    this.receiptService.getInvoice().subscribe((data: string) => {
      this.invoice = this.sanitizer.bypassSecurityTrustHtml(data)
    }, error => {
      console.error('Error fetching invoice', error);
    });
  }

  downloadInvoice(): void {
    this.receiptService.downloadInvoice().subscribe((response: Blob) => {
      const blob = new Blob([response], { type: 'application/pdf' });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = `invoice.pdf`;
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
    }, error => {
      console.error('Error downloading the invoice', error);
    });
  }

}
