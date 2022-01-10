<template>
    <div id="footer">
        <p> <span class="secret">&copy;</span> Orange Team 1  </p>
        <p>Scott Madock, Tim Vogel, Tom Wimer, Dexter Maldonado, August Cassin</p>
    </div>   
</template>

<script>
export default {
    name: 'page-footer'
}
</script>

<style>
    #footer{
        display: flex;
        flex-direction: column;
        text-align: center;
        justify-content: space-evenly;
        background-color: #3E5CBD;
        color: white;
        border-top: 1px solid white;
    }
    .secret{
        
    }
</style>