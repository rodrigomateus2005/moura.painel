import { Directive, OnInit, ElementRef, Optional, ViewChild } from '@angular/core';

import * as $ from 'jquery';
import 'select2';
import { NgModel } from '@angular/forms';

@Directive({
  selector: 'select[appSelect2]'
})
export class Select2Directive implements OnInit {

  private instanceCombo: any;

  private get selectElement(): HTMLSelectElement {
    return this.element.nativeElement;
  }

  constructor(
    private element: ElementRef,
    @Optional() private ngModel: NgModel) {

    if (ngModel) {
      ngModel.valueChanges.subscribe((v) => {
        this.onValueChanged(v);
      });
    }
  }

  ngOnInit(): void {
    this.instanceCombo = $(this.element.nativeElement).select2({
      width: '100%'
    });

    let b = false;
    $(this.element.nativeElement).on('change', (e) => {
      if (!b) {
        b = true;
        this.element.nativeElement.dispatchEvent(new Event('change'));
        b = false;
      }
    });
  }

  public onValueChanged(value: any) {
    if (this.instanceCombo) {
      if (this.selectElement.selectedOptions && this.selectElement.selectedOptions.length > 0) {
        this.instanceCombo.val(this.selectElement.selectedOptions[0].value).trigger('change');
      } else {
        this.instanceCombo.val(null).trigger('change');
      }
    }
  }

}
