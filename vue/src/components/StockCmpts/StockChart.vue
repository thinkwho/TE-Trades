<template>
<div>

  <div class="chart-container">

    <div class="chart-display">
      <!-- TradingView Widget BEGIN -->
      <div class="tradingview-widget-container">
      <div id="tradingview_4d066"></div>
      <div class="tradingview-widget-copyright"><a href="https://www.tradingview.com/symbols/NASDAQ-AAPL/" rel="noopener" target="_blank"><span class="blue-text">AAPL Chart</span></a> by TradingView</div>
      <script type="application/javascript" src="https://s3.tradingview.com/tv.js"></script>
      <script type="application/javascript">
      new TradingView.widget(
      {
      "width": screen.width/3,
      "height": screen.height/2,
      "symbol": "NASDAQ:AAPL",
      "interval": "D",
      "timezone": "Etc/UTC",
      "theme": "light",
      "style": "1",
      "locale": "en",
      "toolbar_bg": "#f1f3f6",
      "enable_publishing": false,
      "allow_symbol_change": true,
      "container_id": "tradingview_4d066"
      }
      );
      </script>
      </div>
      <!-- TradingView Widget END -->
    </div>
  </div>
</div>
</template>

<script>
export default {
  name: 'stock-chart',
  data(){
    return {
      width: this.GetWidth(),
      height: this.GetHeight()
    }
  },
  method:{
    GetWidth(){
      if(screen.width <= 429){
        return screen.width;
      }
      else {
        return screen.width/3;
      }
    },
    getHieght(){
      if(screen.height <= 429){
        return screen.height;
      }
      else {
        return screen.height/3;
      }
    }
  }
}
</script>

<style>

</style>