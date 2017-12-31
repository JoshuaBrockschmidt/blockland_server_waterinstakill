package WaterInstaKillPackage
{
	function Armor::onEnterLiquid(%this, %obj, %coverage, %type)
	{
		Parent::onEnterLiquid(%data, %obj, %coverage, %type);

		if ($WIK::killArmor) {
			// Player dies on entering water
			// Copied from GameMode_SpeedKart
			%obj.hasShotOnce = true;
			%obj.invulnerable = false;
			%obj.damage(%obj, %obj.getPosition(), 10000, $DamageType::Lava);
		}
	}

	function VehicleData::onEnterLiquid(%this, %obj, %coverage, %type)
	{
		Parent::onEnterLiquid(%data, %obj, %coverage, %type);

		if ($WIK::explodeVeh) {
			// Vehicle explodes on entering water
			// Copied from GameMode_SpeedKart
			%obj.damage(%obj, %obj.getPosition(), 10000, $DamageType::Lava);
			%obj.finalExplosion();
		}
	}

};

activatePackage(WaterInstaKillPackage);

// Register Blockland Glass preferences
if(ForceRequiredAddOn("System_BlocklandGlass") != $Error::AddOn_NotFound) {
	registerPref("Water Instant Kill", "General", "Water kills players and bots", "bool", "$WIK::killArmor", "Server_WaterInstaKill", true, "", "", false, false, false);
	registerPref("Water Instant Kill", "General", "Water explodes vehicles", "bool", "$WIK::explodeVeh", "Server_WaterInstaKill", true, "", "", false, false, false);
}

if ($WIK::killArmor $= "")
	$WIK::killArmor = true;
if ($WIK::explodeVeh $= "")
	$WIK::explodeVeh = true;
