﻿using System;
namespace AiWanNet.Requests
{
	public enum RequestType
	{
		Handshake,
		Login,
		Logout,
		GetRoomList,
		JoinRoom,
		AutoJoin,
		CreateRoom,
		GenericMessage,
		ChangeRoomName,
		ChangeRoomPassword,
		ObjectMessage,
		SetRoomVariables,
		SetUserVariables,
		CallExtension,
		LeaveRoom,
		SubscribeRoomGroup,
		UnsubscribeRoomGroup,
		SpectatorToPlayer,
		PlayerToSpectator,
		ChangeRoomCapacity,
		PublicMessage,
		PrivateMessage,
		ModeratorMessage,
		AdminMessage,
		KickUser,
		BanUser,
		ManualDisconnection,
		FindRooms,
		FindUsers,
		PingPong,
		SetUserPosition,
		InitBuddyList = 200,
		AddBuddy,
		BlockBuddy,
		RemoveBuddy,
		SetBuddyVariables,
		GoOnline,
		InviteUser = 300,
		InvitationReply,
		CreateSFSGame,
		QuickJoinGame
	}
}
