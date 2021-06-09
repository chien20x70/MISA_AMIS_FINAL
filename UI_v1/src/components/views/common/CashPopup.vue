<template>
    <div class="popup">
        <div class="model"></div>
        <div class="popup-box">
          <div class="popup-content" :class="{display: changeData === ''}">
            <div class="icon icon-48 exclamation-warning-48"></div>
            <div class="message">{{message}}</div>
          </div>
          <div class="btn-footer" :class="{display: changeData === ''}">
            <button class="btn-No btn-yes" @click="onBtnNoClick">Không</button>
            <button class="btn-add btn-yes" @click="onBtnYesClick">Có</button>
          </div>

          <div class="popup-content" :class="{display: changeData === 'changeData'}">
            <div class="icon icon-48 mi-exclamation-question-48"></div>           
            <div class="message">{{message}}</div>
          </div>
          <div class="btn-footer" :class="{display: changeData === 'changeData'}">
            <button class="btn-No btn-yes" @click="onBtnCancelClick">Hủy</button>
            <button class="btn-No btn-yes" style="margin-left: 130px;" @click="onBtnNoClick">Không</button>
            <button class="btn-add btn-yes" @click="onBtnChangeDataClick">Có</button>
          </div>

          <div class="popup-content" :class="{display: (changeData === 'dataExist' || changeData == 'empty')}">
            <div v-if="changeData == 'dataExist'" class="icon icon-48 exclamation-warning-48"></div>           
            <div v-if="changeData == 'empty'" class="icon icon-48 mi-exclamation-error-48-2"></div>       
            <div class="message">{{message}}</div>
          </div>
          <div class="btn-footer" :class="{display: (changeData === 'dataExist' || changeData == 'empty')}">
            <button class="btn-add btn-yes btn-close" @click="onBtnNoClick">Đóng</button>
          </div>
        </div>
    </div>
</template>
<script>
export default {
  //#region Khai báo Props
    props:{
      refId:{ type: String, selector: null},        // Giá trị EmployeeCode được bind từ EmployeeList
      message: {type: String, default: ''},                 // message thông báo lỗi được bind từ Dialog
      formMode: {type: String, default: ''},
      changeData: {type: String, default: ''},
    },
  //#endregion
    
  //#region METHODS
    methods:{
      /* 
      Click button 'Không' thì đóng popup mà giữ lại form dialog
      CreatedBy: NXCHIEN 17/05/2021
       */
      onBtnNoClick(){
        if (this.changeData == '' || this.changeData == 'dataExist') { //BTN Đóng, Không
          this.$emit("hideCashPopupNotLoad");
        }
        if (this.changeData == 'changeData') {  //BTN Không
          this.$emit("hideCashPopupAndHideDialog");
        }
        if (this.changeData == 'empty') {
          this.$emit("hideCashPopupAndValidate");
        }
        
      },
      onBtnYesClick(){
        if (this.formMode == "CashDialog") {
            this.$emit("hideCashPopupAndRemoveRow");
        }
        if(this.formMode == "CashList"){
            this.axios.delete("/ReceiptPayments/"+ this.refId).then(() =>{
                this.$emit("hideCashPopup");
            }).catch(() =>{})
        }
      },
      onBtnCancelClick(){
        this.$emit("hideCashPopupNotLoad");
      },
      onBtnChangeDataClick(){
        this.$emit("saveChangeData");
      }
    }
  //#endregion
}
</script>
<style scoped>
.popup{
}
.popup .model {
  width: 100%;
  background: rgba(0, 0, 0, 0.4);
  position: fixed;
  left: 0;
  top: 0;
  height: 100%;
  z-index: 102;
  transition: all 0.25s ease;
  opacity: .5; 
}
.popup .popup-box{
  border-radius: 4px;
  width: 444px;
  height: 203px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: white;
  box-shadow: 0 5px 20px 0 rgb(0 0 0 / 10%);
  padding: 32px;
  z-index: 102;
}
.popup-content{
  display: none;
  width: 100%;
  height: 82px;
  align-items: center;
  border-bottom: 1px solid #bbb;
  margin-bottom: 20px;
}

.exclamation-warning-48 {
    background-position: -592px -456px;
}
.message{
  padding: 0px 0 0 16px;
  display: block;
  font-size: 13px;
}
.btn-footer{
  display: none;
  align-items: center;
  justify-content: space-between;
}

.btn-close{
  width: 76px;
  height: 36px;
  background-color: #2ca01c;
  border: none;
  color: white;
  outline: none;
  border-radius: 4px;
  font-weight: 700;
  padding: 8px 20px;
}
.display{
  display: flex;
}
.mi-exclamation-error-48-2 {
    background-position: -24px -954px;
}
</style>