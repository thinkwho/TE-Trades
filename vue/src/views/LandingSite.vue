<template>
    <div id="main-grid">
        <div id="header">
            <landing-site-header />
        </div>
        <div id="main">
            <b-jumbotron>
                <template #header>
                    <div class="flex-container-header">
                        <img class="image" src="../../img/TE TRADES NO BG 2.png" alt="TE Trades Logo">
                        <div class="spacer"><span class="animated a1">T</span><span class="animated a2">E</span>  <span class="animated a3">T</span><span class="animated a4">R</span><span class="animated a5">A</span><span class="animated a6">D</span><span class="animated a7">E</span><span class="animated a8">S</span></div>
                    </div>
                    
                </template>

                <template #lead>
                    <description />
                </template>                
                <page-login /> 
            </b-jumbotron>
        </div>
        <div id="foot">
            <page-footer />
        </div>
    </div>
</template>

<script>
import PageLogin from '../components/LandingPage/PageLogin';
import Description from '../components/LandingPage/Description';
import PageFooter from '../components/PageFooter';
import LandingSiteHeader from '../components/LandingPage/LandingSiteHeader.vue';

export default {
    name: 'LandingSite',
    components:{
        PageLogin,
        Description,
        PageFooter,
        LandingSiteHeader,
    }
}
</script>

<style scoped>
* {
    font-family: "fira sans";
    font-weight: 400;
    color: white;
    backface-visibility: hidden;
}
.spacer{
    margin-left: 20px;
}
.animated{
    animation: movedownbounce 1s ease-out;
    animation-fill-mode: both;
    backface-visibility: hidden;
    display: inline-block;
    opacity: 0;
    font-size: 6rem;
    padding-top: 8px;
}
.a2{
    animation-delay: .6s;
}
.a3{
    animation-delay: 1.2s;
}
.a4{
    animation-delay: 1.8s;
}
.a5{
    animation-delay: 2.4s;
}
.a6{
    animation-delay: 3s;
}
.a7{
    animation-delay: 3.6s;
}
.a8{
    animation-delay: 4.2s;
}
.image{
    max-height: 6rem;
    align-self: center;
    padding-left: 8px;
    padding-right: 8px;
}
div#stock{
    grid-area: stock;
}
div#main{
    border: solid;
    grid-area: main;
    background-color: #253670;
    background-image: linear-gradient(#253670,#475070, #99AEF2);
    animation: fadein .75s ease-out;
    box-shadow: 14px 7px 7px rgba(20, 20, 20, .6);
    margin: 5vh;
}
div#foot{
    grid-area: foot;
    background-color: #3E5CBD;
    border-color: white;
    border-top: solid 2px;
}
div#header{
    grid-area: head;
    background-color: #253670;
    border-color: white;
    border-bottom: solid 2px;
}
#main-grid{
    display: grid;
    height: 100vh;
    grid-template-columns: 1fr 5fr 1fr;
    grid-template-rows: .5fr 5fr 1fr;
    gap: 1em;
    grid-template-areas: 
    "head head head head"
    "main main main main"
    "foot foot foot foot";
    background-color: #253670;
    background-image: url("../../img/Skyline.jpg");
    background-size:100% 100%;
}
.flex-container-header{
    display: flex;
    flex-direction: row;
}

@media screen and (max-width: 1279px){
    #main-grid{
        grid-template-columns: 1fr 1fr;
        grid-template-rows: .5fr 5fr 1fr;
        grid-template-areas:
        "head head"
        "main main"
        "foot foot";
    }
}

@media screen and (max-width: 768px){
    #main-grid{
        grid-template-columns: 1fr;
        grid-template-rows: .5fr 5fr 1fr;
        grid-template-areas:
        "head"
        "main"
        "foot";
    }
    .animated{
        font-size: 3.5rem;
    }
}
@keyframes fadein {
    0% {
        opacity: 0;
    }
    100% {
        opacity: 100;
    }
}

@keyframes movedownbounce {
    0% {
        opacity: 0;
        transform: translateY(-50px);
    }
    100% {
        opacity: 100;
        transform: translateY(0px);
    }
}
</style> 