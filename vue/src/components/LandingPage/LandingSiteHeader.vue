<template>
    <div>
        <b-row>
            <stock-ticker />
        </b-row>
    </div>
</template>

<script>
import StockTicker from '../../components/StockTicker.vue'
export default {
    name: "landing-site-header",
    components:{
        StockTicker
    },
    methods: {
        showAbout(){
            this.$store.commit("HOME_MAIN_DESCRIPTION");
        },
        showTitleScreen(){
            this.$store.commit("HOME_MAIN_TITLE");
        },
        showHowToPlay(){
            this.$store.commit("HOME_MAIN_HOW_TO_PLAY");
        }
    }
}
</script>

<style scoped>
    .website-name{
        display: flex;
        flex-direction: row;
    }
    .header{
        display: flex;
        text-align: center;
        padding-left: 12px;
        padding-right: 4px;
        font-size: 32px;
        font-weight: 300;
        line-height: 16px;
    }
    .flex-container{
        display: flex;
        justify-content: space-around;
        flex-direction: row;
        margin-left: auto;
    }
    .image{
        max-height: 4.4rem;
        align-self: center;
    }
    .yuppie{
        align-self: center;
        font-size: 3.5rem;
        padding-left: 20px;
        color: #FFEFB0;
        text-shadow: 0 0 3px #B39314;
        flex-wrap: nowrap;
    }
    @media screen and (max-width: 768px){
    .yuppie{
        font-size: 1.7rem;
    }
    .btn{
        width: 65px;
        height: 60px;
        align-self: center;
        text-align: center;
        margin-right: 12px;
        font-size: 12px;
    }
}
</style>