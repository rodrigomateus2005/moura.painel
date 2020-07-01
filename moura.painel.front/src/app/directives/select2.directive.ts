import { Directive, OnInit, ElementRef, Optional, ViewChild } from '@angular/core';

import * as $ from 'jquery';
import 'select2';
import { NgModel } from '@angular/forms';

@Directive({
  selector: 'select[appSelect2]'
})
export class Select2Directive implements OnInit {

  // @ViewChild(NgModel)
  // private $ngModel: NgModel;
  // public get ngModel(): NgModel {
  //   return this.$ngModel;
  // }
  // public set ngModel(value: NgModel) {
  //   this.$ngModel = value;

  //   if (value) {
  //     value.valueChanges.subscribe((v) => {
  //       this.onValueChanged(v);
  //     });
  //   }
  // }

  private instanceCombo: any;

  constructor(private element: ElementRef,
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
      this.instanceCombo.val(value).trigger('change');
    }
  }

}
