<template>
    <div class="select-custom" :class="{'invisible': selectState}">
        <div class="item" v-for="(item, index) in lists" :key="index" :value="item.value" @click="btnSelectClick(item.value)" :class="{'color': item.value == saveValue}">{{item.content}}</div>      
    </div>
</template>

<script>
export default {
    //#region Khai báo
    props:{
        selectState:{ type: Boolean, selector: false}               // Giá trị để hiển thị ComboboxFilter phần phân trang
    },
    data(){
        return{
            lists: [{ content: '10 bản ghi trên 1 trang', value: 10},
                   { content: '20 bản ghi trên 1 trang', value: 20},   
                   { content: '30 bản ghi trên 1 trang', value: 30},
                   { content: '50 bản ghi trên 1 trang', value: 50},
                   { content: '100 bản ghi trên 1 trang', value: 100}
                ],
            saveValue: null,        // Giá trị của thẻ div được click
        }
    },
    //#endregion
    //#region METHODS
    methods:{
        /* 
        Khi chọn 1 dòng trong thẻ thì bind giá trị sang bên EmployeeList
        Lưu giá trị của dòng được click để bind class color vào.
        CreatedBy: NXCHIEN 17/05/2021
        */
        btnSelectClick(value){
            this.$emit('passValueToSelect', value);
            this.saveValue = value;
        },
        
    },
    //#endregion
    
}
</script>
<style scoped>
.select-custom{
    position: absolute;
    width: 200px;
    height: 166px;
    top: calc(100% - 212px);
    min-width: 200px;
    background-color: white;
    border: 1px solid #bbb;
}
.select-custom .item{
    height: 32px;
    width: 100%;
    padding: 0 14px 0 10px;
    text-align: left;
    line-height: 32px;
    cursor: pointer;
}
.item:hover{
    color: #2ca01c;
    background-color: rgb(219, 219, 219);
}
.invisible{
    display: none;
}
.color{
    background-color: #2ca01c;
    color: white;
}
</style>