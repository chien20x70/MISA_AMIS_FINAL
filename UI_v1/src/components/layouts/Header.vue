<template>
    <div class="header">
        <div class="header-left">
            <div class="flex header-branch2">
                <div class="header-branch-name">CÔNG TY TNHH SẢN XUẤT-THƯƠNG MẠI-DỊCH VỤ QUI PHÚC</div>
                <div class="icon icon-24 chevron-right"></div>
            </div>
        </div>
        <div class="flex header-branch">
            <button class="button">
                <div class="flex button-text">
                    <div class="icon icon-24 bell__nav"></div>
                </div>
            </button>
            <div class="icon icon-24 user-avatar"></div>
            <div class="header-branch-name1">Nguyễn Xuân Chiến</div>
            <div class="icon icon-24 chevron-right"></div>
        </div>
    </div>
</template>
<script>
export default {
    
}
</script>
<style scoped>
.header{
    height: 48px;
    display: flex !important;
    align-items: center;
    justify-content: space-between;
}
.header-left{
    display: flex;
    align-items: center;
    justify-content: space-around;
    width: 456px;
    margin-left: 22px;
}
.header .header-branch{
    display: flex;
    align-items: center ;
    cursor: pointer;
    width: 240px;
    justify-content: space-between;
    margin-right: 10px;
}
.header .header-branch2{
    display: flex;
    align-items: center ;
    cursor: pointer;
    width: 395px;
    justify-content: space-around;
    
}
.header-branch-name{
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    width: 364px;
}
.flex{
    display: flex !important;
    align-items: center !important;
}
.header .space-beetween{
    width: 20%;
}
.header .header-input{
    height: 32px;
    display: flex;
    align-items: center;
}
.margin{
    margin-right: 10px;
}
.button{
    height: 100%;
    position: relative;
    transition: all .2s ease;
    border: 0;
    width: 44px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
    color: #fff;
    box-sizing: border-box;
    background: 0 0;
    overflow: visible;
}
.button-text{
    font-weight: 600;
    position: relative;
    color: inherit;
    display: inline-block;
    transition: all .25s ease;
    white-space: nowrap;
    font-size: 13px;
    line-height: 13px;
    justify-content: center;
    align-items: center;
}

.user-avatar {
    background-position: -352px -894px;
    width: 32px;
    height: 32px;
    border-radius: 50%;
}
.chevron-right {
    background-position: -76px -357px;
    transform: rotate(90deg); 
}
.display{
    display: none;
}
</style>