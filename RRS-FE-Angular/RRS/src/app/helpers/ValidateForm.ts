import { FormControl, FormGroup } from "@angular/forms";

export default class ValidateForm{
    
    static validateFormField(formgroup:FormGroup){
        Object.keys(formgroup.controls).forEach(feild=>{
          const control=formgroup.get(feild);
          if(control instanceof FormControl){
            control?.markAsDirty({onlySelf:true});
          }
          else if(control instanceof FormGroup){
            this.validateFormField(control)
    
          }
        })
      }
}