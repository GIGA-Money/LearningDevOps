import { Component, Input, OnInit, Output } from '@angular/core';
import { EventEmitter } from 'events';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
@Output() actionEmitter = new EventEmitter();
@Input() loggedIn: boolean = false;

submit(action: string){
  this.actionEmitter.emit(action);
}
  constructor() { }

  ngOnInit(): void {
  }

}
