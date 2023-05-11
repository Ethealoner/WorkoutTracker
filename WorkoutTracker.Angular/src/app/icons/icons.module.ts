import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeatherModule } from 'angular-feather';
import { PlusCircle, ChevronDown, Save, Delete } from 'angular-feather/icons';

const icons = {
  PlusCircle, ChevronDown, Save, Delete
};

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FeatherModule.pick(icons)
  ],
  exports: [
    FeatherModule
  ]
})
export class IconsModule { }
