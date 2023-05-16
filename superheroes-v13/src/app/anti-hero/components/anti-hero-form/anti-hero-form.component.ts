import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { AntiHero } from '../../models/anti-hero.interface';

@Component({
  selector: 'app-anti-hero-form',
  templateUrl: './anti-hero-form.component.html',
  styleUrls: ['./anti-hero-form.component.scss'],
})
export class AntiHeroFormComponent implements OnInit {
  @Input() selectedId = '';
  @Input() actionButtonLabel: string = 'Create';
  @Input() selectedAntiHero: AntiHero | null = null;
  @Output() action = new EventEmitter();
  form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      id: [''],
      firstName: [''],
      lastName: [''],
      house: [''],
      knownAs: [''],
    });
  }

  ngOnInit(): void {
    this.checkAction();
  }

  checkAction() {
    if (this.selectedAntiHero) {
      this.actionButtonLabel = 'Update';
      this.patchDataValues();
    }
  }

  patchDataValues() {
    this.action.emit({
      value: this.form.value,
      action: this.actionButtonLabel,
    });
  }

  emitAction() {
    this.action.emit({
      value: this.form.value,
      action: this.actionButtonLabel,
    });
  }

  clear() {
    this.form.reset();
  }
}
