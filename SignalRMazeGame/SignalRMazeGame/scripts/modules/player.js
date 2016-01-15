'use strict';

function Player(element, width, gameHub) {
    this.element = element;
    this.width = width;
    this.gameHub = gameHub;
};

Player.prototype.move = function (left, top) {
    var position = {
        left: parseInt(this.element.css('left')) + left,
        top: parseInt(this.element.css('top')) + top,
    };

    if (left) {
        this.element.css('left', position.left + 'px');
    }
    if (top) {
        this.element.css('top', position.top + 'px');
    }

    // Notify other clients.
    this.gameHub.server.move(JSON.stringify(position));
};

Player.prototype.moveTo = function (left, top) {
    this.element.css('left', left + 'px');
    this.element.css('top', top + 'px');
};