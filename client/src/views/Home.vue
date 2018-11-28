<template>
  <div class="home">
    <h1>Float Management System</h1>
    <form>
      <label for="floatInput">
        <p>Enter float to join</p>
      </label>
      <input type="text" name="floatInput" id="floatInput" v-model="floatInput">
      <input type="button" value="Select Float" @click="setFloat">
    </form>
    <br>
    <p>Current Float</p>
    <h5>{{value}}</h5>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { HubConnection, TransportType } from "@aspnet/signalr";

export default Vue.extend({
  data: () => ({
    value: "",
    floatInput: "",
    apiBase: "https://localhost:5001"
  }),

  async beforeMount () {
    try {
      const hubConnection = new HubConnection(this.apiBase + '/Hubs/Push', {
        transport: TransportType.WebSockets
      });
      hubConnection.on("Push", (workstation: string) => {
        if (this.value.toLowerCase() === workstation.toLowerCase()) {
          alert('Push Notification: Float ' + workstation + ' is joined by another user.');
        }
        this.value = this.floatInput;
      });
      hubConnection.start();
    } catch {
      alert("Unable to connect to API");
    }
  },

  methods: {
    async setFloat () {
      try {
        await fetch(this.apiBase + '/api/push', {
          body: JSON.stringify(this.floatInput),
          headers: new Headers({
            "content-type": "application/json"
          }),
          method: "POST"
        });
      } catch {
        alert("Unable to add value to API");
      }
    }
  }
});
</script>
