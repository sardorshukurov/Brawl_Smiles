mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
    console.log("Hello, world!");
  },

  RateGame: function () {
    ysdk.feedback.canReview()
    .then(({ value, reason }) => {
      if (value) {
        ysdk.feedback.requestReview()
        .then(({ feedbackSent }) => {
          myGameInstance.SendMessage("Game Manager", "TurnOffRateButton");
        })
      } else {
        myGameInstance.SendMessage("Game Manager", "TurnOffRateButton");
      }
    })
  },

  ShowAdv: function () {
    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          console.log("the ad was shown");
        },
        onError: function(error) {
          // some action on error
        }
    }
    })
  },

  AddCoins: function () {
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage("Game Manager", "AddCoinsForAd");
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
          myGameInstance.SendMessage("Game Manager", "OnAdSeen");
        }
    }
})
  },
});