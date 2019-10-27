import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-modal-dialog',
  templateUrl: './modal-dialog.component.html',
  styleUrls: ['./modal-dialog.component.css']
})
export class ModalDialogComponent implements OnInit {

  @Input() header: string;
	@Input() description: string;
  @Output() isConfirmed: EventEmitter<boolean> = new EventEmitter<boolean>();
  
  ngOnInit(){
    
  }
	private confirm() {
		this.isConfirmed.emit(true);
	}
	private close() {
		this.isConfirmed.emit(false);
	}

}
