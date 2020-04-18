import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumToArray'
})
export class EnumToArrayPipe implements PipeTransform {

  transform(value: any): any {
    const result = Object.keys(value).filter(e => !isNaN(+e)).map(o => { return {index: +o, name: value[o]}});
    return result;
  }

}
