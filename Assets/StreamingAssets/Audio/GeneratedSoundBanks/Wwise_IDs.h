/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID BATTLE_PLAY = 2534386424U;
        static const AkUniqueID BATTLE_STOP = 611866442U;
        static const AkUniqueID CREDIT_PLAY = 1445688307U;
        static const AkUniqueID CREDIT_STOP = 575810265U;
        static const AkUniqueID WIN_AXOLOTL_PLAY = 55536622U;
        static const AkUniqueID WIN_ROBOT_PLAY = 1339265993U;
        static const AkUniqueID WIN_STOP = 16679072U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace WINNING
        {
            static const AkUniqueID GROUP = 2996998095U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID WIN_AXOLOTL = 3565959805U;
                static const AkUniqueID WIN_ROBOT = 1619244480U;
            } // namespace STATE
        } // namespace WINNING

    } // namespace STATES

    namespace SWITCHES
    {
        namespace WIN_SCENE
        {
            static const AkUniqueID GROUP = 3095016256U;

            namespace SWITCH
            {
                static const AkUniqueID WIN_AXOLOTL = 3565959805U;
                static const AkUniqueID WIN_ROBOT = 1619244480U;
            } // namespace SWITCH
        } // namespace WIN_SCENE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID SS_AIR_FEAR = 1351367891U;
        static const AkUniqueID SS_AIR_FREEFALL = 3002758120U;
        static const AkUniqueID SS_AIR_FURY = 1029930033U;
        static const AkUniqueID SS_AIR_MONTH = 2648548617U;
        static const AkUniqueID SS_AIR_PRESENCE = 3847924954U;
        static const AkUniqueID SS_AIR_RPM = 822163944U;
        static const AkUniqueID SS_AIR_SIZE = 3074696722U;
        static const AkUniqueID SS_AIR_STORM = 3715662592U;
        static const AkUniqueID SS_AIR_TIMEOFDAY = 3203397129U;
        static const AkUniqueID SS_AIR_TURBULENCE = 4160247818U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID BATTLE_AND_WIN = 1950213306U;
        static const AkUniqueID CREDIT_MUSIC = 832601146U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
